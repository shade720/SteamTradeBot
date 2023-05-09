import "./App.css"
import React from 'react';
import SideBar from "./components/SideBar/SideBar";
import Content from "./components/Content/Content";
import Header from "./components/Header/Header";

const App = (props) =>
{
    return (
        <div className='app-wrapper'>
            <SideBar />
            <Content log={props.log} state={props.state} settingsPreset={props.settingsPreset} />
            <Header />
        </div>
    );
}


export default App;
