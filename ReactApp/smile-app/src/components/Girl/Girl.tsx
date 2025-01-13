import { FC } from "react";
import { IGirl } from "../../models/IGirl";
import "./Girl.css";

type GirlProps = {
  girl: IGirl;
  onDelete?: (id: number) => void;
  isPreview?: boolean;
};

const Girl: FC<GirlProps> = ({ girl, onDelete, isPreview = false }) => {
  const deleteGirl = () => {
    onDelete?.(girl.id);
  };
  return (
    <div className={(isPreview ? "preview" : "") + " girl"}>
      <div className="girl-name">
        {girl.name} [{girl.likesCount}]
      </div>
      <div className="image-container">
        <img src={girl.url}></img>
      </div>
      {onDelete && (
        <div>
          <button onClick={deleteGirl}>delete</button>
        </div>
      )}
    </div>
  );
};

export {Girl};
