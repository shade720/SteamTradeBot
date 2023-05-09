import SettingItem from "./SettingItem/SettingItem"
import Button from "./../../Button/Button"
import styles from "./Settings.module.css"

const Settings = () => {
    return (
        <div className={styles.pane}>
            <div className={styles.settingsPane}>
                <div>
                    <h2>Logic settings</h2>
                    <SettingItem name='Trend: ' />
                    <SettingItem name='Sales: ' />
                    <SettingItem name='Cancel range: ' />
                    <SettingItem name='Listing find range: ' />
                    <SettingItem name='Analysis period: ' />
                    <SettingItem name='Buy quantity: ' />
                    <SettingItem name='Required profit: ' />
                </div>
                <div>
                    <h2>Pipeline settings</h2>
                    <SettingItem name='Min price: ' />
                    <SettingItem name='Max price: ' />
                    <SettingItem name='Pipeline size: ' />
                </div>
            </div>
            <div className={styles.buttonPane}>
                <Button name='Submit'/>
            </div>
        </div>);
}

export default Settings;