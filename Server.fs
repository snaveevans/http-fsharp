module Cranberry.Server

open Microsoft.FSharp.Control
open System
open System.Threading
open System.Net
open System.Net.Sockets
open Cranberry.StreamHelper

type Server = TcpListener

let bar = "HTTP/1.1 200 OK
Server: Cranberry
Date: Fri, 06 Sep 2019 21:34:44 GMT
Content-Type: text/html
Content-Length: 17

<div>foobar</div>"

let handleClient (client: TcpClient) = async {
    let! bytes = client |> clientToBytes
    let request = bytes |> bytesToString |> stringToRequest
    printfn "%A" request 
    let response = bar |> Text.Encoding.UTF8.GetBytes
    do! (client, response) |> sendData
    client.Close()
}

let serve (server: Server) = async {
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
    let work = server |> serve
    Async.RunSynchronously(work)

