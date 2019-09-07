# Cranberry

## Pipeline

1. stream/TcpClient
2. bytes
3. string
4. string[]
5. `request`
6. process & `response`
7. bytes
8. stream

## Types:

```fsharp
request: { Method: string; Uri: string; HTTPv: string; Headers: Header[] Body: Option<'a> }
```

```fsharp
response: { HTTPv: string; Status: string; Message: string; Body: Option<'a> }
```

## Notes:
- leave body as bytes
- `BodyProcessors`: transform body from byte[] into X & X => { byte[], ?headers }
