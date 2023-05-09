import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import App from './App';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const root = createRoot(rootElement);

let state = [
    { settingName : 'Connection',         value : 'disconnected'},
    { settingName: 'Status',              value: 'working' },
    { settingName: 'Items Sold',            value: '34' },
    { settingName: 'Items Bought',          value: '82' },
    { settingName: 'Analyzed',  value: '1451' },
    { settingName: 'Uptime',              value: '23:41.21' }
]

root.render(<BrowserRouter basename={baseUrl}><App state={state} /></BrowserRouter>);
