import { useState } from "react";
import StateLabel from "./StateLabel/StateLabel";
import Button from "./../../Button/Button"
import styles from "./Work.module.css"



const Work = () => {
    const [connection, setConnection] = useState('connected')
    const [status, setStatus] = useState('disabled')
    const [itemsSold, setItemSold] = useState('10')
    const [itemsBought, setItemBought] = useState('15')
    const [analyzed, setAnalyzed] = useState('1504')
    const [workTime, setworkTime] = useState('23.45:30')

    return (
        <div className={styles.panel}>
            <div>
                <h3>Logs</h3>
                <textarea readonly className={styles.console} placeholder='some text...'></textarea>
            </div>
            <div className={styles.statePanel}>
                <h3>State</h3>
                <div className={styles.statusPanel}>
                    <StateLabel name='Connection: ' value={ connection }/>
                    <StateLabel name='Status: ' value={ status }/>
                    <StateLabel name='Items sold: ' value={ itemsSold } />
                    <StateLabel name='Items bought: ' value={ itemsBought } />
                    <StateLabel name='Analyzed items: ' value={ analyzed } />
                    <StateLabel name='Work time: ' value={ workTime } />
                </div>
                <h3>Control</h3>
                <div className={styles.controlPanel}>
                    <Button name='Start'/>
                    <Button name='Stop'/>
                    <Button name='Restart' />
                    <Button name='Reset pipeline' />
                    <Button name='Connect' />
                    <Button name='Disconnect' />
                </div>
            </div>
        </div>
    );
}

export default Work;