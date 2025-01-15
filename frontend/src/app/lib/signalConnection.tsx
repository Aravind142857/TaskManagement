import * as signalR from "@microsoft/signalr"

export const connectSignalR = async (): Promise<signalR.HubConnection> => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5001/collaborationHub")
        .withAutomaticReconnect()
        .build();

    await connection.start();
    console.log("SignalR Connected");
    return connection;
}