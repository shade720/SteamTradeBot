import styles from "./SettingItem.module.css"

const SettingItem = (props) => {
    return (
        <div className={styles.item}>
            <label>{props.name}</label>
            <input></input>
        </div>);
}

export default SettingItem;