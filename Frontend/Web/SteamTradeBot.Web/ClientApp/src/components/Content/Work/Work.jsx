import styles from "./Work.module.css"
import ServiceStatePanel from "./ServiceStatePanel/ServiceStatePanel";
import Console from "./Console/Console";
import ControlPanel from "./ControlPanel/ControlPanel";

const Work = (props) => {
    return (
        <div className={styles.workPanel}>
            <Console text={props.log} />
            <ServiceStatePanel state={ props.state } />
            <ControlPanel />
        </div>
    );
}

export default Work;