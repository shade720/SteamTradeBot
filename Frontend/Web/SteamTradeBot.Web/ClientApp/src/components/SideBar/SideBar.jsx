import { NavLink } from "react-router-dom";
import styles from "./SideBar.module.css"

const SideBar = () => {
    return (
        <nav className={styles.sideBar}>
            <h2>
                <NavLink className={styles.title} to='/work'>SteamTradeBot</NavLink>
            </h2>
            <div className={styles.linksPane}>
                <NavLink className={styles.link} to='/work'>Work</NavLink>
                <NavLink className={styles.link} to='/settings'>Settings</NavLink>
                <NavLink className={styles.link} to='/stats'>Stats</NavLink>
            </div>
        </nav>);
}

export default SideBar;