import axios from "axios";
import { IGirl } from "../models/IGirl";
import { IGirlViewModel } from "../dtos/IGirlViewModel";

async function GetGirls(): Promise<IGirl[]> {
  const response = await axios.get(
    "https://localhost:7130/api/ApiGirl/GetGirls"
  );

  const girlDtos = response.data as IGirlViewModel[];
  return girlDtos.map((dto) => {
    return {
      id: dto.id,
      name: dto.name,
      url: dto.imageSrc,
      likesCount: dto.likeCount,
    } as IGirl;
  });
}

async function CreateGirl(girl: IGirl) {
  axios.post("https://localhost:7130/api/ApiGirl/CreateGirlForGuess", girl);
}

const apiGirls = { GetGirls, CreateGirl };

export { apiGirls };
