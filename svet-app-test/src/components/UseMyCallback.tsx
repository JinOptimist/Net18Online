import React, { useCallback, useState } from "react";

function Likes() {
  console.log("LikesDizlikes component rendered. And added like.");

  const [like, setLikes] = useState<number>(0); // Инициализация состояния
  const [dizlike, setDizlikes] = useState<number>(0); // Инициализация состояния

  // Функция для увеличения числа "лайков"
  // если так писать, функция пересоздаётся при рендере, см. ниже как не пересоздавать при рендере
  //   const incrementLikes = () => {
  //     setNumber((prevLike) => prevLike + 1); // Обновление состояния
  //   };
  // если исп.useCallback, то функция не пересоздаётся при рендере, только при изменении числа.
  const incrementLikes = useCallback(() => {
    setLikes((prevLike) => prevLike + 1);
  }, []);

  const incrementDizlikes = useCallback(() => {
    setDizlikes((dizlike) => dizlike + 1);
  }, []);

  /* можно указать, при изменении чего , будет пересоздана функция, но так НЕ безопасное обновления состояния
     использовать текущее состояние prevLike
  const incrementLikes = useCallback(() => {
    setLikes((prevLike+1);
  }, [prevLike]);*/

  return (
    <div className="LikesDizlikes">
      <div>
        <div>Likes: {like}</div>
        <button onClick={incrementLikes}>Add Like</button>
      </div>
      <div>
        <div>Dizlikes: {dizlike}</div>
        <button onClick={incrementDizlikes}>Add Dizlike</button>
      </div>
    </div>
  );
}

export default Likes;
