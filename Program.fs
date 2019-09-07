// Learn more about F# at http://fsharp.org
module Cranberry.Main

open Cranberry.Server
open Cranberry.StreamHelper

[<EntryPoint>]
let main argv = 
    printfn "Arguments: %d" argv.Length
    let server = ("127.0.0.1", 8080) |> createServer
    startServer(server)
    0 // return an integer exit code
