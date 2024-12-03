use std::fs::File;
use std::io;
use std::io::prelude::*;
use std::io::BufReader;

fn main() -> io::Result<()> {
    let file = File::open("input.txt")?;
    let reader = BufReader::new(file);

    let mut count = 0;

    reader
        .lines()
        .filter_map(|line| line.ok())
        .for_each(|content| {
            let mut nums = content
                .split_whitespace()
                .filter_map(|word| word.parse::<i32>().ok());
            let mut check = 0;
            let mut prev_diff = 0;
            let mut prev = nums.next().unwrap();
            for curr in nums.clone() {
                let mut diff = prev - curr;
                if diff.abs() > 3 {
                    break;
                }
                prev = curr;
                if diff == 0 {
                    break;
                }
                diff /= diff.abs();

                if diff > 0 && prev_diff < 0 {
                    break;
                }
                if diff < 0 && prev_diff > 0 {
                    break;
                }
                check += 1;
                prev_diff = diff;
            }
            if check == nums.count() {
                println!("{content}");
                count += 1;
            }
        });
    println!("{count}");
    Ok(())
}
