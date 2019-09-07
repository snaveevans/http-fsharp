module Cranberry.StreamHelper

open Microsoft.FSharp.Control
open System
open System.Threading
open System.Net.Sockets

let tenMegaBytes = 10000000

let readData (client: TcpClient) = async {
    let stream = client.GetStream()
    let length = (client.ReceiveBufferSize, tenMegaBytes) |> Math.Min
    let data = Array.zeroCreate length
    let cancellationSource = new CancellationTokenSource()
    let asyncResult = (data, 0, length, cancellationSource.Token) |> stream.ReadAsync
    do! Async.AwaitIAsyncResult(asyncResult) |> Async.Ignore
    return data
    }

let sendData (client: TcpClient, data: byte[]) =
    let stream = client.GetStream()
    stream.Write(data, 0, data.Length)
