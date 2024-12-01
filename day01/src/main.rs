use std::fs::File;
use std::io;
use std::io::prelude::*;
use std::io::BufReader;

fn main() -> io::Result<()> {
    let file = File::open("input.txt")?;
    let reader = BufReader::new(file);

    let mut list_a: Vec<i32> = Vec::new();
    let mut list_b: Vec<i32> = Vec::new();

    for line in reader.lines() {
        match line {
            Ok(content) => {
                let mut parts = content.split_whitespace();
                list_a.push(parts.nth(0).unwrap().parse::<i32>().unwrap());
                list_b.push(parts.last().unwrap().parse::<i32>().unwrap());
            }
            Err(e) => eprintln!("Error reading line: {e}"),
        }
    }
    list_a.sort();
    list_b.sort();

    let mut dist: i32 = 0;
    for (a, b) in list_a.iter().zip(list_b.iter()) {
        dist += (a - b).abs();
    }
    println!("{dist}");

    Ok(())
}
