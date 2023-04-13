import "./App.css"
import React from 'react';
import SideBar from "./components/SideBar/SideBar";
import Content from "./components/Content/Content";

const App = () =>
{
    return (
        <div className='app-wrapper'>
            <SideBar />
            <Content />
        </div>
    );
}


export default App;
