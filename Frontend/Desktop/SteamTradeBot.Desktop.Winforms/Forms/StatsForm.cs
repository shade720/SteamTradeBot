using System.Windows.Forms.DataVisualization.Charting;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer;
using SteamTradeBot.Desktop.Winforms.BusinessLogicLayer.ServiceAccess;
using SteamTradeBot.Desktop.Winforms.Models;

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
        _signalRClient.OnStateRefreshEvent += RefreshInfo;
        _signalRClient.OnHistoryRefreshEvent += RefreshCharts;
        var initTradingHistory = await _restClient.GetInitHistory();
        InitTradingChart(initTradingHistory);
        InitBudgetChart(initTradingHistory);
        InitUptimeChart(initTradingHistory);
    }

    private void StatsForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        _signalRClient.OnStateRefreshEvent -= RefreshInfo;
        _signalRClient.OnHistoryRefreshEvent -= RefreshCharts;
    }

    #region ChartsInit

    private void InitTradingChart(IEnumerable<TradingEvent> history)
    {
        var boughtSeries = GetSpecifiedSeries("Items bought", Color.Orange);
        var soldSeries = GetSpecifiedSeries("Items sold", Color.Chartreuse);
        var canceledSeries = GetSpecifiedSeries("Items canceled", Color.Gray);

        var a = history
            .OrderBy(x => x.Time)
            .GroupBy(x => x.Type)
            .Select(x => (x.Key, x.GroupBy(x => x.Time.Date)))
            .Select(x =>
            {
                switch (x.Key)
                {
                    case InfoType.ItemAnalyzed:
                        break;
                    case InfoType.ItemBought:
                        foreach (var a in x.Item2)
                            boughtSeries.Points.AddXY(a.Key, a.Count());
                        break;
                    case InfoType.ItemSold:
                        foreach (var a in x.Item2)
                            soldSeries.Points.AddXY(a.Key, a.Count());
                        break;
                    case InfoType.ItemCanceled:
                        foreach (var a in x.Item2)
                            canceledSeries.Points.AddXY(a.Key, a.Count());
                        break;
                    case InfoType.Error:
                        break;
                    case InfoType.Warning:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return true;
            })
            .ToList();
        TradingChart.Series.Clear();
        TradingChart.Series.Add(boughtSeries);
        TradingChart.Series.Add(soldSeries);
        TradingChart.Series.Add(canceledSeries);

        TradingChart.Legends[0].Font = new Font(new FontFamily("Segoe UI"), 10, FontStyle.Regular);
    }

    private void InitBudgetChart(IEnumerable<TradingEvent> history)
    {
        var budgetSeries = GetSpecifiedSeries("Balance", Color.Yellow);
        var balancePerDay = history
            .OrderBy(x => x.Time)
            .GroupBy(x => x.Time.Date)
            .Select(x => (x.Key, x.Max(x => x.CurrentBalance)));
        foreach (var item in balancePerDay)
        {
            budgetSeries.Points.AddXY(item.Key, item.Item2);
        }
        BudgetChart.Series.Clear();
        BudgetChart.Series.Add(budgetSeries);
        BudgetChart.Legends[0].Font = new Font(new FontFamily("Segoe UI"), 10, FontStyle.Regular);

        var maxBalance = balancePerDay.Max(x => x.Item2);
        if (maxBalance > 0)
        {
            BudgetChart.ChartAreas[0].AxisY.Minimum = 0;
            BudgetChart.ChartAreas[0].AxisY.Maximum = 2 * maxBalance;
        }
    }

    private void InitUptimeChart(IEnumerable<TradingEvent> history)
    {
        var uptimeSeries = GetSpecifiedSeries("Uptime", Color.DodgerBlue);

        var uptimePerDay = history
            .OrderBy(x => x.Time)
            .GroupBy(x => x.Time.Date)
            .Select(x => (x.Key, x.GroupBy(x => x.Time.Hour)))
            .Select(x => (x.Key, x.Item2.Count() / 24.0 * 100));
        foreach (var uptime in uptimePerDay)
        {
            uptimeSeries.Points.AddXY(uptime.Key, uptime.Item2);
        }

        UptimeChart.Series.Clear();
        UptimeChart.Series.Add(uptimeSeries);
        UptimeChart.Legends[0].Font = new Font(new FontFamily("Segoe UI"), 10, FontStyle.Regular);
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

    private void RefreshInfo(StateInfo stateInfo)
    {
        
    }

    private void RefreshCharts(TradingEvent tradingEvent)
    {
        ThreadHelperClass.ExecOnForm(this, () =>
        {
            switch (tradingEvent.Type)
            {
                case InfoType.ItemAnalyzed:
                    BudgetChart.Series["Balance"].Points.ElementAt(^1).SetValueY(tradingEvent.CurrentBalance);
                    BudgetChart.Refresh();
                    break;
                case InfoType.ItemBought:
                    var previousBoughtValue = TradingChart.Series["Items bought"].Points.ElementAt(^1).YValues.Max();
                    TradingChart.Series["Items bought"].Points.ElementAt(^1).SetValueY(previousBoughtValue + 1);
                    TradingChart.Refresh();
                    break;
                case InfoType.ItemSold:
                    var previousSoldValue = TradingChart.Series["Items sold"].Points.ElementAt(^1).YValues.Max();
                    TradingChart.Series["Items sold"].Points.ElementAt(^1).SetValueY(previousSoldValue + 1);
                    TradingChart.Refresh();
                    break;
                case InfoType.ItemCanceled:
                    var previousCanceledValue = TradingChart.Series["Items canceled"].Points.ElementAt(^1).YValues.Max();
                    TradingChart.Series["Items canceled"].Points.ElementAt(^1).SetValueY(previousCanceledValue + 1);
                    TradingChart.Refresh();
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
}
