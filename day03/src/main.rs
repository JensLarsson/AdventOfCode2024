use regex::Regex;
use std::fs::File;
use std::io;
use std::io::prelude::*;
use std::io::BufReader;

fn main() -> io::Result<()> {
    let file = File::open("input.txt")?;
    let reader = BufReader::new(file);

    let mut _total = 0;
    let mut mult = 1;

    let regex = Regex::new(r"mul\((\d+),(\d+)\)|(do|don't)\b").unwrap();

    reader
        .lines()
        .filter_map(|line| line.ok())
        .for_each(|content| {
            //let caps = regex.captures_iter(&content);
            let caps = regex.captures_iter(&content);
            for ele in caps {
                if let Some(a_str) = ele.get(1) {
                    let a = ele.get(1).unwrap().as_str().parse::<i32>().unwrap();
                    let b = ele.get(2).unwrap().as_str().parse::<i32>().unwrap();
                    _total += a * b * mult;
                } else if let Some(op) = ele.get(3) {
                    mult = if op.as_str() == "do" { 1 } else { 0 };
                }
            }
        });

    println!("{_total}");

    Ok(())
}
