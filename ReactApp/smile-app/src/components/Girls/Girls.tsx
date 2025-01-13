import { useCallback, useEffect, useState } from "react";
import { IGirl } from "../../models/IGirl";
import "./Girls.css";
import { CreateGirl, Girl } from "..";
import { apiGirls } from "../../api.services";

function Girls() {
  const [girls, setGirls] = useState<IGirl[]>([]); // ~view models

  useEffect(() => {
    apiGirls.GetGirls().then(setGirls);
  }, []);

  const getMaxGirlId = useCallback(() => {
    const ids = girls.map((g) => g.id);
    return Math.max(...ids);
  }, [girls]);

  const createGirl = useCallback((girl: IGirl) => {
	apiGirls.CreateGirl(girl);
    setGirls((oldGirls) => [...oldGirls, girl]);
  }, []);

  const onDeleteGirl = useCallback((id: number) => {
    setGirls((oldGirls) => oldGirls.filter((g) => g.id != id));
  }, []);

  return (
    <div>
      <h1>Girl 1</h1>

      <div className="girls">
        {girls.map((girl) => (
          <Girl key={girl.id} girl={girl} onDelete={onDeleteGirl}></Girl>
        ))}
        <CreateGirl getMaxGirlId={getMaxGirlId} createGirl={createGirl} />
      </div>
    </div>
  );
}

export { Girls };
