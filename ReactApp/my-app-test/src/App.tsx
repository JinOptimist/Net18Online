import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes, Link, useLocation } from 'react-router-dom';
import AnimeGirl from "./components/AnimeGirl/AnimeGirl";
import EcologyChat from "./components/Ecology/EcologyChat";
import Home from './components/HomePage/Home';

import './App.css';

const MenuContext = React.createContext({
    menuVisible: true,
    hideMenu: () => {},
    showMenu: () => {}
});

function App() {
    const [menuVisible, setMenuVisible] = useState(true);
    const hideMenu = () => setMenuVisible(false);
    const showMenu = () => setMenuVisible(true);

    return (
        <Router>
            <MenuContext.Provider value={{ menuVisible, hideMenu, showMenu }}>
                <div>
                    <Menu />
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/animeGirl" element={<AnimeGirl />} />
                        <Route path="/ecologyChat" element={<EcologyChat />} />
                    </Routes>
                    <HomeButton />
                </div>
            </MenuContext.Provider>
        </Router>
    );
}

function Menu() {
    const { menuVisible, hideMenu, showMenu } = React.useContext(MenuContext);
    const location = useLocation();

    useEffect(() => {
        if (location.pathname === '/') {
            showMenu();
        } else {
            hideMenu();
        }
    }, [location, showMenu, hideMenu]);

    if (!menuVisible) return null;

    return (
        <nav>
            <ul>
                <li>
                    <Link to="/" onClick={hideMenu}>Home</Link>
                </li>
                <li>
                    <Link to="/animeGirl" onClick={hideMenu}>AnimeGirl</Link>
                </li>
                <li>
                    <Link to="/ecologyChat" onClick={hideMenu}>EcologyChat</Link>
                </li>
            </ul>
        </nav>
    );
}

function HomeButton() {
    const location = useLocation();
    if (location.pathname === '/') {
        return null;
    }
    return (
        <div className="home-button">
            <Link to="/">Home</Link>
        </div>
    );
}

export default App;
