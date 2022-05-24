import { serverEnter } from "./BuyersService";

const isInitOccured = "isInitOccured";

const isGuest = "isGuest";
const buyerId = "memberId";

export async function initSession() {
  const response = serverEnter();
  try {
    console.log(response)
    const didInit = localStorage.getItem(isInitOccured);
    console.log("dafsf" + didInit)
    if (didInit === null) {
      const id = await response;

      localStorage.setItem(buyerId, String(id));
      localStorage.setItem(isGuest, "true");
      localStorage.setItem(isInitOccured, "true");
      console.log("initiated")
      
    }
  } catch (e) {
    alert("Sorry, an unkown error has occured!");
    window.close();
  }

  
}

type Primitive = string | number | boolean;
function createGetter<T extends Primitive>(
  convert: (value: string | null | undefined) => T,
  field: string
): () => T {
  return () => convert(localStorage.getItem(field));
}

function createSetter<T extends Primitive>(field: string): (v: T) => void {
  return (newValue: T) => localStorage.setItem(field, String(newValue));
}

// Guest setter Getter
export const getIsGuest = createGetter(Boolean, isGuest);

export const setIsGuest: (v: boolean) => void = createSetter(isGuest);

// member setter Getter
export const getBuyerId = createGetter(Number, buyerId);

export const setBuyerId: (v: number) => void = createSetter(buyerId);
