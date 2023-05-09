import styles from "./Console.module.css"

const Console = (props) => {
    return (
        <div className={styles.consolePanel}>
            <h3>Logs</h3>
            <textarea className={styles.console} readonly placeholder='some text...'>{/*props.log*/}</textarea>
        </div>
    );
}

export default Console;