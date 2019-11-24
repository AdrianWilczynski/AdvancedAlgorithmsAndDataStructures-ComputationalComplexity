open System

[<Literal>]
let Infinity = Int32.MaxValue
[<Literal>]
let NoParent = -1;
[<Literal>]
let NotConnected = 0;

type Edge = { StartVertex: int; EndVertex: int; Weight: int; }
type Graph = { VertexCount: int; Edges: Edge list }

let readGraphs() =
    let readNumbers() =
        Console.ReadLine().Split " "
        |> Array.map Int32.Parse

    seq { 
        let graphCount = 
            match readNumbers() with
            | [| graphCount |] -> graphCount
            | _ -> failwith "Expected one argument: graph count"

        for _ in 1..graphCount do
            let vertexCount, edgeCount = 
                match readNumbers() with
                | [| vertexCount; edgeCount |] -> vertexCount, edgeCount
                | _ -> failwith "Expected two arguments: vertex and edge counts"

            let readEdges edgeCount =
                seq { 
                    for _ in 1..edgeCount do
                        match readNumbers() with
                        | [| startVertex; endVertex; weight |] -> { StartVertex = startVertex; EndVertex = endVertex; Weight = weight; }
                        | _ -> failwith "Expected three arguments: edge start vertex, end vertex and weight"
                } 
                |> Seq.toList

            { VertexCount = vertexCount; Edges = readEdges edgeCount }
    } |> Seq.toList

let toAdjacencyMatrix graph =
    let matrix = Array2D.create<int> graph.VertexCount graph.VertexCount NotConnected
    
    for edge in graph.Edges do
        matrix.[edge.StartVertex - 1, edge.EndVertex - 1] <- edge.Weight

    matrix

let findShortestPaths(adjacencyMatrix: int [,]) =
    let vertexCount = adjacencyMatrix.GetLength(0)

    let distances = Array.create vertexCount Infinity
    let included = Array.create vertexCount false
    let parents = Array.create vertexCount NoParent

    let findCheapest() =
        [ 0..vertexCount - 1 ]
        |> Seq.filter(fun vertex -> distances.[vertex] <> Infinity && not included.[vertex])
        |> Seq.fold(fun cheapest vertex ->
            match cheapest with
            | None -> Some vertex
            | Some cheapest when distances.[vertex] < distances.[cheapest] -> Some vertex
            | _ -> cheapest)
            None

    distances.[0] <- 0

    let mutable hasNext = true

    while hasNext do
        let maybeCheapest = findCheapest()

        if maybeCheapest.IsSome then
            let cheapest = maybeCheapest.Value
            let cheapestCost = distances.[cheapest]
            included.[cheapest] <- true

            for vertexIndex in 0..vertexCount - 1 do             
                let isConnected =  adjacencyMatrix.[cheapest, vertexIndex] <> NotConnected

                let costFromCheapest = adjacencyMatrix.[cheapest, vertexIndex]
                let sumCost = cheapestCost +  costFromCheapest

                if not included.[vertexIndex] && isConnected && sumCost < distances.[vertexIndex] then
                    parents.[vertexIndex] <- cheapest
                    distances.[vertexIndex] <- sumCost
        else
            hasNext <- false

    distances, parents, vertexCount

let rec readPath (parents: int[]) vertex =
    let parent = parents.[vertex]

    match parent with
    | NoParent -> [ vertex ]
    | _ ->  readPath parents parent @ [ vertex ]  


let printSolution graphs =
    let printableVertexNumber vertex =
        vertex + 1

    let mutable index = 1;
    for graph in graphs do
        if index <> 1 then
            printfn ""

        let (distances: int[]), parents, vertexCount = graph
        
        printf "Graf nr %i" index
        
        for vertex in 1..vertexCount - 1 do
            let path = readPath parents vertex |> Seq.map printableVertexNumber
            let distance = distances.[vertex]

            printfn ""
            if Seq.length path = 1 then
                printf "NIE ISTNIEJE DROGA Z %i DO %i" 1 (printableVertexNumber vertex)
            else
                printf  "%s %i" (String.Join("-", path)) distance
            
        index <- index + 1

[<EntryPoint>]
let main _ =
    readGraphs()
    |> Seq.map (toAdjacencyMatrix >> findShortestPaths)
    |> printSolution

    0 // return an integer exit code