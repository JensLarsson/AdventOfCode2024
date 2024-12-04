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
            let length = nums.clone().count() as i32;
            for i in 0..length {
                let mut num_range: Vec<i32> = nums.clone().collect();
                num_range.remove(i as usize);
                if check(num_range) {
                    println!("{content}");
                    count += 1;
                    break;
                }
            }
        });
    println!("{count}");
    Ok(())
}

fn check(mut nums: Vec<i32>) -> bool {
    let mut skip = true;
    let mut check = 0;
    let mut prev_diff = 0;
    let mut prev = nums.first().unwrap().clone();
    nums.remove(0);
    for curr in nums.clone() {
        let mut diff = prev - curr;
        if diff.abs() > 3 {
            break;
        }
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
        prev = curr;
        check += 1;
        prev_diff = diff;
    }
    if check == nums.len() {
        return true;
    }
    false
}
