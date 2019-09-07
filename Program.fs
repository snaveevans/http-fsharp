// Learn more about F# at http://fsharp.org
module Cranberry.Main

open System
open System.Threading
open System.Net
open System.Net.Sockets
open Microsoft.FSharp.Control
open Cranberry.StreamHelper
open Cranberry.Server

[<EntryPoint>]
let main argv = 
    printfn "Arguments: %d" argv.Length
    let server = ("127.0.0.1", 8080) |> createServer
    startServer(server)
    // let client = server.AcceptTcpClient()
    // printfn "Connected"
    // let! data = client |> readData
    // printfn "Read data %d" data.Length
    // let str = data |> Text.Encoding.UTF8.GetString
    // printfn "Request"
    // printfn "%s" str
    // let response = bar |> Text.Encoding.UTF8.GetBytes
    // printfn "Sending response %s" bar
    // (client, response) |> sendData
    // client.Close()
    // printfn "Connection closed"
    0 // return an integer exit code
