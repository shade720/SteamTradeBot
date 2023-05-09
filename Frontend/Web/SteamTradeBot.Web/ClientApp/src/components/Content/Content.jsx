import styles from "./Content.module.css"
import Settings from "./Settings/Settings"
import { Route, Routes } from "react-router-dom"
import Work from "./Work/Work";

const Content = (props) => {
    return (
        <div className={styles.content} >
            <Routes>
                <Route path='/work' element={<Work log={props.log} state={props.state} />} />
                <Route path='/settings' element={<Settings settingPreset={props.settingPreset} />} />
            </Routes>
        </div>
    );
}

export default Content;