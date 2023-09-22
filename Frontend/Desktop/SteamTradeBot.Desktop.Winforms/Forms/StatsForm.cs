using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;
using SteamTradeBot.Desktop.Winforms.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SteamTradeBot.Desktop.Winforms.Forms;

public partial class StatsForm : Form
{
    private readonly SteamTradeBotRestClient _restClient;
    private readonly SteamTradeBotSignalRClient _signalRClient;

    public StatsForm(
        SteamTradeBotRestClient restClient,
        SteamTradeBotSignalRClient signalRClient)
    {
        InitializeComponent();
        _restClient = restClient;
        _signalRClient = signalRClient;
    }

    private async void StatsForm_Load(object sender, EventArgs e)
    {
        _signalRClient.OnHistoryRefreshEvent += RefreshCharts;
        var initTradingHistory = await _restClient.GetInitHistory();
        InitInfoTable(initTradingHistory);
        RenderChartsByTable();
    }

    private void StatsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _signalRClient.OnHistoryRefreshEvent -= RefreshCharts;
    }

    #region Init

    private void InitInfoTable(IEnumerable<TradingEvent> history)
    {
        var eventsPerDay = history
            .GroupBy(x => x.Time.Date)
            .OrderBy(x => x.Key);

        foreach (var dayEvents in eventsPerDay)
        {
            var day = dayEvents.Key.ToLocalTime().Date;
            var bought = dayEvents.Count(x => x.Type == InfoType.ItemBought);
            var sold = dayEvents.Count(x => x.Type == InfoType.ItemSold);
            var canceled = dayEvents.Count(x => x.Type == InfoType.ItemCanceled);
            var uptime = dayEvents.GroupBy(x => x.Time.Hour).Count() / 24.0 * 100;
            var balance = dayEvents.Max(x => x.CurrentBalance);
            InfoTable.Rows.Add(day.ToString("dd.MM.yyyy"), bought, sold, canceled, $"{uptime}%", balance);
        }
    }

    private void RenderChartsByTable()
    {
        var boughtSeries = GetSpecifiedSeries("Items bought", Color.Orange);
        var soldSeries = GetSpecifiedSeries("Items sold", Color.Chartreuse);
        var canceledSeries = GetSpecifiedSeries("Items canceled", Color.Gray);

        var balanceSeries = GetSpecifiedSeries("Balance", Color.Gold);

        var uptimeSeries = GetSpecifiedSeries("Uptime", Color.DodgerBlue);

        foreach (var row in InfoTable.Rows.OfType<DataGridViewRow>().Where(x => x.Visible))
        {
            var day = DateTime.Parse(row.Cells[0].Value.ToString());

            boughtSeries.Points.AddXY(day, row.Cells[1].Value);
            soldSeries.Points.AddXY(day, row.Cells[2].Value);
            canceledSeries.Points.AddXY(day, row.Cells[3].Value);

            balanceSeries.Points.AddXY(day, row.Cells[5].Value);

            uptimeSeries.Points.AddXY(day, double.Parse(row.Cells[4].Value.ToString().Replace("%", "")));
        }

        UptimeChart.Series.Clear();
        UptimeChart.Series.Add(uptimeSeries);
        UptimeChart.Legends[0].Font = new Font(new FontFamily("Segoe UI"), 10, FontStyle.Regular);

        BalanceChart.Series.Clear();
        BalanceChart.Series.Add(balanceSeries);
        BalanceChart.Legends[0].Font = new Font(new FontFamily("Segoe UI"), 10, FontStyle.Regular);

        TradingChart.Series.Clear();
        TradingChart.Series.Add(boughtSeries);
        TradingChart.Series.Add(soldSeries);
        TradingChart.Series.Add(canceledSeries);
        TradingChart.Legends[0].Font = new Font(new FontFamily("Segoe UI"), 10, FontStyle.Regular);

        UptimeChart.Refresh();
        BalanceChart.Refresh();
        TradingChart.Refresh();
    }

    private static Series GetSpecifiedSeries(string name, Color color)
    {
        return new Series
        {
            Name = name,
            Color = color,
            BorderColor = color,
            XValueType = ChartValueType.DateTime,
            YValueType = ChartValueType.Int32,
            ChartType = SeriesChartType.Line,
            BorderWidth = 2,
        };
    }

    #endregion

    #region RefreshingData

    private void RefreshCharts(TradingEvent tradingEvent)
    {
        ThreadHelperClass.ExecOnForm(this, () =>
        {
            switch (tradingEvent.Type)
            {
                case InfoType.ItemAnalyzed:
                    InfoTable.Rows[^1].Cells[5].Value = tradingEvent.CurrentBalance;
                    BalanceChart.Series["Balance"].Points.ElementAt(^1).SetValueY(tradingEvent.CurrentBalance);
                    BalanceChart.Refresh();
                    break;
                case InfoType.ItemBought:
                    var previousBoughtValue = int.Parse(InfoTable.Rows[^1].Cells[1].Value.ToString());
                    TradingChart.Series["Items bought"].Points.ElementAt(^1).SetValueY(previousBoughtValue + 1);
                    TradingChart.Refresh();
                    InfoTable.Rows[^1].Cells[1].Value = previousBoughtValue + 1;
                    break;
                case InfoType.ItemSold:
                    var previousSoldValue = int.Parse(InfoTable.Rows[^1].Cells[2].Value.ToString());
                    TradingChart.Series["Items sold"].Points.ElementAt(^1).SetValueY(previousSoldValue + 1);
                    TradingChart.Refresh();
                    InfoTable.Rows[^1].Cells[2].Value = previousSoldValue + 1;
                    break;
                case InfoType.ItemCanceled:
                    var previousCanceledValue = int.Parse(InfoTable.Rows[^1].Cells[3].Value.ToString());
                    TradingChart.Series["Items canceled"].Points.ElementAt(^1).SetValueY(previousCanceledValue + 1);
                    TradingChart.Refresh();
                    InfoTable.Rows[^1].Cells[3].Value = previousCanceledValue + 1;
                    break;
                case InfoType.Error:
                    break;
                case InfoType.Warning:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        });
    }

    #endregion

    private void RefreshButton_Click(object sender, EventArgs e)
    {
        var rowSetToHide = InfoTable.Rows.OfType<DataGridViewRow>();
        foreach (var row in rowSetToHide)
        {
            if (DateTime.Parse(row.Cells[0].Value.ToString()) < FromDatePicker.Value ||
                DateTime.Parse(row.Cells[0].Value.ToString()) > ToDatePicker.Value)
                row.Visible = false;
            else
                row.Visible = true;
        }

        RenderChartsByTable();
    }
}
