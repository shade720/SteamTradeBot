import StateLabel from "./StateLabel/StateLabel";
import styles from "./ServiceStatePanel.module.css"

const ServiceStatePanel = (props) => {

    let stateLabels = props.state.map(state => <StateLabel name={`${state.settingName}: `} value={state.value} />);
    return (
        <div className={styles.serviceStatePanel}>
            <h3>State</h3>
            <div className={styles.statePanelGroupBox}>
                {stateLabels}
            </div>
        </div>
    );
}

export default ServiceStatePanel;