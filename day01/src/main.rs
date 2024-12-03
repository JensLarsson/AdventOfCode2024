use std::collections::HashMap;
use std::fs::File;
use std::io;
use std::io::prelude::*;
use std::io::BufReader;

fn main() -> io::Result<()> {
    let file = File::open("input.txt")?;
    let reader = BufReader::new(file);

    let mut list_a: Vec<i32> = Vec::new();
    let mut list_b: Vec<i32> = Vec::new();

    reader
        .lines()
        .filter_map(|line| line.ok())
        .for_each(|content| {
            let mut parts = content.split_whitespace();
            if let (Some(first), Some(last)) = (parts.next(), parts.last()) {
                if let (Ok(a), Ok(b)) = (first.parse::<i32>(), last.parse::<i32>()) {
                    list_a.push(a);
                    list_b.push(b);
                }
            }
        });

    list_a.sort();
    list_b.sort();

    let mut dist: i32 = 0;
    for (a, b) in list_a.iter().zip(list_b.iter()) {
        dist += (a - b).abs();
    }
    println!("PartA: The distance between the lists is:{dist}");

    let mut map: HashMap<i32, (i32, i32)> = HashMap::new();

    for num in &list_a {
        map.insert(*num, (0, 0));
    }

    for num in &list_a {
        if let Some((_, right)) = map.get_mut(&num) {
            *right += 1;
        }
    }

    for num in &list_b {
        if let Some((left, _)) = map.get_mut(&num) {
            *left += 1;
        }
    }

    let mut similarity: i32 = 0;
    for (key, (left, right)) in &map {
        similarity += key * left * right;
    }
    println!("PartB: The similarity between the lists is: {similarity}");
    Ok(())
}
