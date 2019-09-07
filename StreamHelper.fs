module Cranberry.StreamHelper

open Microsoft.FSharp.Control
open System
open System.Threading
open System.Net.Sockets

type Header = { Name: string; Value: string; }
type Request = { Method: string; Uri: string; Headers: Header[]; Body: string; }

let tenMegaBytes = 10000000

let clientToBytes (client: TcpClient) = async {
    let stream = client.GetStream()
    let length = (client.ReceiveBufferSize, tenMegaBytes) |> Math.Min
    let data = Array.zeroCreate length
    let cancellationSource = new CancellationTokenSource()
    let asyncResult = (data, 0, length, cancellationSource.Token) |> stream.ReadAsync
    do! asyncResult |> Async.AwaitIAsyncResult |> Async.Ignore
    return data
}

let bytesToString (bytes: byte[]) =
    let str = bytes |> Text.Encoding.UTF8.GetString
    str.Split("\n")

let stringToRequest (strArr: string[]) =
    let firstLine = strArr.[0].Split(" ")
    let httpMethod = firstLine.[0]
    let uri = firstLine.[1]
    // let httpVersion = firstLine.[2]
    { Method = httpMethod; Uri = uri; Headers = [||]; Body = "" }

let sendData (client: TcpClient, data: byte[]) = async {
    let stream = client.GetStream()
    let asyncResult = (data, 0, data.Length) |> stream.WriteAsync
    do! asyncResult |> Async.AwaitIAsyncResult |> Async.Ignore
}

