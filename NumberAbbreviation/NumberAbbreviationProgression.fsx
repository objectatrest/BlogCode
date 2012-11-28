//Turn a number like 4,000,000 into abbreviation 4M. useful in financial reporting applications

//1) hardcoded rules. simple and easy.
let abbreviate_number_hardcoded (num:decimal) = 
    match num with
    | v when 1000m <= v && v < 1000000m || -1000000m < v && v <= -1000m -> sprintf "%MK" (num / 1000m)
    | v when 1000000m <= v && v < 1000000000m || -1000000000m < v && v <= -1000000m -> sprintf "%MM" (num / 1000000m)
    | v when 1000000000m <= v && v < 1000000000000m || -1000000000000m < v && v <= -1000000000m -> sprintf "%MB" (num / 1000000000m)
    | _ -> sprintf "%M" num

printfn "%s" (abbreviate_number_hardcoded 40150m)
printfn "%s" (abbreviate_number_hardcoded -40150m)
printfn "%s" (abbreviate_number_hardcoded 12005000m)
printfn "%s" (abbreviate_number_hardcoded -12005000m)
printfn "%s" (abbreviate_number_hardcoded 32100000000m)
printfn "%s" (abbreviate_number_hardcoded -32100000000m)


//2) separate data and behavior. separate data from algorithm so we can tease out a general algorithm
type AbbreviationRange = { Min:decimal; Max:decimal; Abbreviation:string }

let abbreviate_number_behavior (ranges:list<AbbreviationRange>) (num:decimal) = 
    let possible_abbreviations = ranges |> List.filter (fun abbrev_rule -> abbrev_rule.Min <= num && num < abbrev_rule.Max 
                                                                            || -1m*abbrev_rule.Max < num && num <= -1m*abbrev_rule.Min)
    if List.isEmpty possible_abbreviations then
        sprintf "%M" num
    else
        let abbrev = List.head possible_abbreviations
        let short_num = num / abbrev.Min
        sprintf "%M%s" short_num abbrev.Abbreviation

let abbreviate_number_from_rules = abbreviate_number_behavior [
                                                        { Min=1000m; Max=1000000m; Abbreviation="K" };
                                                        { Min=1000000m; Max=1000000000m; Abbreviation="M" };
                                                        { Min=1000000000m; Max=1000000000000m; Abbreviation="B" };
                                                    ]

printfn "%s" (abbreviate_number_from_rules 40150m)
printfn "%s" (abbreviate_number_from_rules -40150m)
printfn "%s" (abbreviate_number_from_rules 12005000m)
printfn "%s" (abbreviate_number_from_rules -12005000m)
printfn "%s" (abbreviate_number_from_rules 32100000000m)
printfn "%s" (abbreviate_number_from_rules -32100000000m)

//extensions:active patterns
;;