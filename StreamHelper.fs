module Cranberry.StreamHelper

open System
open System.Threading
open System.Net.Sockets
open Microsoft.FSharp.Control

let tenMegaBytes = 10000000

let readData (client: TcpClient) = async {
    let stream = client.GetStream()
    let length = (client.ReceiveBufferSize, tenMegaBytes) |> Math.Min
    let data = Array.zeroCreate length
    let cancellationSource = new CancellationTokenSource()
    let asyncResult = (data, 0, length, cancellationSource.Token) |> stream.ReadAsync
    let async = Async.AwaitIAsyncResult(asyncResult) |> Async.Ignore
    do! async
    // stream.Read(data, 0, length) |> ignore
    return data
    }

let sendData (client: TcpClient, data: byte[]) =
    let stream = client.GetStream()
    stream.Write(data, 0, data.Length)
