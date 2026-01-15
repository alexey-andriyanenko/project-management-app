import { useEffect, useRef } from "react";

export interface IUseDebounce {
  (delay: number): (callback: VoidFunction) => void;
}

export const useDebounce: IUseDebounce = (delay) => {
  const timerRef = useRef<NodeJS.Timeout>();

  const resetDebounce = () => {
    if (timerRef.current) clearTimeout(timerRef.current);
  };

  useEffect(() => {
    return () => resetDebounce();
  }, []);

  return (callback: VoidFunction) => {
    resetDebounce();
    timerRef.current = setTimeout(callback, delay);
  };
};
