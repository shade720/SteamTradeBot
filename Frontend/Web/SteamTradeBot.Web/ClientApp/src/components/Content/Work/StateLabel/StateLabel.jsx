import styles from "./StateLabel.module.css"

const StateLabel = (props) => {
    return (
        <div className={styles.stateLabel}>
            <label className={styles.label}>{props.name}</label>
            <p className={styles.value}>{props.value};</p>
        </div>
    );
}

export default StateLabel;