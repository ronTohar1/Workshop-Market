import MarketNotification from "../DTOs/MarketNotification"

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