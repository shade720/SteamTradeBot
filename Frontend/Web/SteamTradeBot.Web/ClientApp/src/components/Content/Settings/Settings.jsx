import SettingItem from "./SettingItem/SettingItem"
import Button from "./../../Button/Button"
import styles from "./Settings.module.css"

const Settings = () => {
    return (
        <div className={styles.pane}>
            <div className={styles.settingsPane}>
                <SettingItem name='Первая строка: ' />
                <SettingItem name='Вторая строка: ' />
                <SettingItem name='Третья строка: ' />
            </div>
            <div className={styles.buttonPane}>
                <Button name='Submit'/>
            </div>
        </div>);
}

export default Settings;