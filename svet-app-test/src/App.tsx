import React from "react";
import logo from "./logo.svg";
import logo2 from "./img/photo_2024-10-16_19-23-10.jpg";
import "./App.css";
import SmileTest from "./components/SVET";
import СountFridaysUntilNewYear from "./components/Fridys";
//import Likes from "./components/Likes";
import Likes from "./components/UseMyCallback";
import Boys from "./components/animeBoys";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <img src={logo2} className="App-logoJoke" alt="logoJoke" />
        <p></p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          <SmileTest></SmileTest>
        </a>
      </header>
      <div>До Нового года осталось</div>
      <СountFridaysUntilNewYear></СountFridaysUntilNewYear>
      <div>пятниц.</div>
      <div>
        <Likes></Likes>
      </div>
      <div className="Boys">
        <Boys></Boys>
      </div>
    </div>
  );
}

export default App;
