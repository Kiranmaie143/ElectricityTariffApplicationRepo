import React from 'react';
import './App.css';
import TariffList from './components/TariffList.jsx';

const App = () => {
    return (
        <div className="App">
            <header className="App-header">
                <TariffList /> {/* Include the TariffList component */}
            </header>
        </div>
    );
};

export default App;