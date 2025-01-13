import { ChangeEventHandler, useCallback, useState } from "react";
import "./Respondent.css";

function Respondent() {
    const [name, setName] = useState<string>("Ivan");
    const [surveyName, setSurveyName] = useState<string>("Опрос 1");
  
    return (
      <div className="respondent-container">
        <div className="name">
          Имя: {name}
        </div>
        <div className="survey-name">
          Название опроса: {surveyName}
        </div>
      </div>
    );
  }
  
  export default Respondent;