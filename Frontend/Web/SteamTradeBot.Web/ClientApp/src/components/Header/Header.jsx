import { NavLink } from "react-router-dom";
import styles from "./Header.module.css"

const Header = () => {
    return (
        <div className={styles.header}>
            <NavLink className={ styles.signInLink }>Sign In</NavLink>
        </div>);
}

export default Header;