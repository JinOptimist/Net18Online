import { FC, useCallback, useEffect, useState } from "react";
import { IGirl } from "../../models/IGirl";
import "./CreateGirl.css";
import { Girl } from "..";

type CreateGirlProps = {
  getMaxGirlId: () => number;
  createGirl: (girl: IGirl) => void;
};

const CreateGirl: FC<CreateGirlProps> = ({ getMaxGirlId, createGirl }) => {
  const [newGirlName, setNewGirlName] = useState<string>("");
  const [newGirlUrl, setNewGirlUrl] = useState<string>("");
  const [girl, setGirl] = useState<IGirl>({} as IGirl);

  const addGirl = useCallback(() => {
    const newId = getMaxGirlId() + 1;
    const newGirl = {
      likesCount: 0,
      name: newGirlName,
      url: newGirlUrl,
      id: newId,
    } as IGirl;
    createGirl(newGirl);
    setNewGirlName("");
    setNewGirlUrl("");
  }, [createGirl, getMaxGirlId, newGirlName, newGirlUrl]);

  useEffect(() => {
    setGirl((oldGirl) => {
      return { ...oldGirl, name: newGirlName, url: newGirlUrl };
    });
  }, [newGirlName, newGirlUrl]);

  return (
    <div className="new-girl-creation">
      <Girl girl={girl} isPreview={true} />
      <div className="new-girl-creation-info">
        <input
          value={newGirlName}
          onChange={(evt) => setNewGirlName(evt.currentTarget.value)}
          placeholder="Имя новой девочки"
        ></input>
        <input
          value={newGirlUrl}
          onChange={(evt) => setNewGirlUrl(evt.currentTarget.value)}
          placeholder="Путь к картинке"
        ></input>
        <button onClick={addGirl}>Add</button>
      </div>
    </div>
  );
};

export { CreateGirl };
