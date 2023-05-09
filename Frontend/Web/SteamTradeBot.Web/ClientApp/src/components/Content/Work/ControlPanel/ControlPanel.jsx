import Button from "./../../../Button/Button"
import styles from "./ControlPanel.module.css"

const ControlPanel = () => {
    return (
        <div className={styles.controlPanel}>
            <h3>Control</h3>
            <div className={styles.controlPanelGroupBox}>
                <Button name='Start' />
                <Button name='Stop' />
                <Button name='Restart' />
                <Button name='Reset pipeline' />
                <Button name='Connect' />
                <Button name='Disconnect' />
            </div>
        </div>
    );
}

export default ControlPanel;