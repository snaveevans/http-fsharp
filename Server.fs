module Cranberry.Server

open System
open System.Threading
open System.Net
open System.Net.Sockets
open Microsoft.FSharp.Control
open Cranberry.StreamHelper

type Server = TcpListener

let bar = "HTTP/1.1 200 OK
Server: Suave (https://suave.io)
Date: Fri, 06 Sep 2019 21:34:44 GMT
Content-Type: text/html
Content-Length: 9

Hello GET"

let handleClient (client: TcpClient) = async {
    let! data = client |> readData
    printfn "Read data %d" data.Length
    let str = data |> Text.Encoding.UTF8.GetString
    printfn "Request"
    printfn "%s" str
    let response = bar |> Text.Encoding.UTF8.GetBytes
    printfn "Sending response %s" bar
    (client, response) |> sendData
    client.Close()
    printfn "Connection closed"
}

let workflow (server: Server) = async {
    while true do
        let! client = server.AcceptTcpClientAsync() |> Async.AwaitTask
        let handler = client |> handleClient
        Async.Start(handler)
}

let createServer (address: string, port: int) =
    let localAddr = address |> IPAddress.Parse
    (localAddr, port) |> TcpListener

let startServer (server: Server) =
    server.Start()
    let work = server |> workflow
    Async.RunSynchronously(work)

