import MarketNotification from "../DTOs/MarketNotification"
export const dummyNotificaitons = [
    new MarketNotification(0, "Why did u not answer~{\"\n\"} me dude?asdfa dsf asdf asdf asdf asd fasdf af"),
    new MarketNotification(1, "Lets buy shoes"),
    new MarketNotification(2, "Hey man"),
    new MarketNotification(3, "Best project dude"),

]

export const noteConn: NoteWebSocket = { ws: null, notifications: [], setUp: false }

export interface NoteWebSocket {
    ws: WebSocket | null
    notifications: string[]
    setUp: boolean
}

export function clearNotifications() {
    noteConn.notifications = []
}


export function setUpConnection(url: string, memberId: number) {
    noteConn.ws = new WebSocket(url)
    noteConn.setUp = true
    noteConn.ws.addEventListener('message', function (e: any) { 
        alert("Added one")
        noteConn.notifications.push("another one") })
}

export function addNotificationListener(event: (msg: string) => void) {
    if (noteConn.setUp)
        noteConn.ws?.addEventListener('message', function (e: any) { event(e.data) })
}