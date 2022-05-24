const isGuest = "isGuest";
const memberId = "memberId";

export function initSessions() {
  localStorage.setItem(memberId, "-1");
  localStorage.setItem(isGuest, "true");
}

type Primitive = string | number | boolean
function createGetter<T extends Primitive>(convert:(value:string | null | undefined) => T, field:string): () => T{
    return () => convert(localStorage.getItem(field));
}

function createSetter<T extends Primitive>(field:string): (v:T) => void {
    return (newValue:T) => localStorage.setItem(field, String(newValue));
}

// Guest setter Getter
export const getIsGuest = createGetter(Boolean,isGuest)

export const setIsGuest : (v: boolean) => void = createSetter(isGuest)

// member setter Getter

export const getMemberId = createGetter(Number,memberId);

export const setMemberId : (v: number) => void =  createSetter(memberId)