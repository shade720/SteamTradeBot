import styles from "./Content.module.css"
import Settings from "./Settings/Settings"
import { Route, Routes } from "react-router-dom"
import Work from "./Work/Work";

const Content = () => {
    return (
        <div className={styles.content} >
            <Routes>
                <Route path='/work' element={<Work />} />
                <Route path='/settings' element={<Settings />} />
            </Routes>
        </div>
    );
}

export default Content;