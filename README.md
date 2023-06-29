# Prune

Prune lets you programmatically remove older files while retaining certain files at given time intervals.. It is developed using .NET 7 and can be compiled for x64 (Windows, Linux, MacOS) and arm64 (Raspberry Pi).

So far it is running in the wild on a Debian 12 VM, a Raspberry Pi 4B 2GB and 4GB, and a Raspberry Pi 3B.

## Usage

When you run prune, it is highly recommended you use the `--verbose` and `--dry-run` flags until you can confirm what will be pruned.

Below is an example usage:

```bash
prune --help
```

```bash
prune --path /backups/images --verbose --dry-run /
   --ext img /
   --keep-last 5 /
   --keep-daily 3 /
   --keep-weekly 2 /
   --keep-monthly 4 /
```

> Use `--verbose` and `--dry-run` flags until you confirm what will be pruned

The following time interval options are available:
| Interval | Description |
| ---------- | ---------------------------- |
| `keep-last` | Keep the last number of files |
| `keep-hourly` | Keep files for a number of hours |
| `keep-daily` | Keep files for a number of days |
| `keep-weekly` |Keep files for a number of weeks |
| `keep-monthly` |Keep files for a number of months |
| `keep-yearly` |Keep files for a number of years |

> If there is more than one backup for a time interval, only the latest is kept

The time intervals are processed in the same order as above. Each interval only covers backups within its segment. A single backup will not be matched by multiple time intervals, and if a time interval spans over a previous interval, it will prune until it finds a matching unique file. For example, if the last daily time interval backup is on a Thursday, the weekly time interval will skip to the preceding week to find a file, pruning any other files it iterates through.

The backups are designed to follow the same logic as Proxmox Backup Server pruning. [Use their very useful simulator](https://pbs.proxmox.com/docs/prune-simulator/) to test different configurations.

## Build

To build from source, you will need to have:

- [DotNet SDK for .Net 7.0](https://dotnet.microsoft.com/en-us/download)
- Git

Pull down this repo and you can compile the project from the `src/` folder.

```bash
# linux-x64 environment
dotnet publish --runtime linux-x64 --self-contained -p:PublishSingleFile=true -c Release -o ./out
```

```bash
# linux-arm64 environment
dotnet publish --runtime linux-arm64 --self-contained -p:PublishSingleFile=true -c Release -o ./out
```

```bash
# win-x64 environment
dotnet publish --runtime win-x64 --self-contained -p:PublishSingleFile=true -c Release -o ./out
```

You should see the binary in the `/src/out` folder. You can discard the `.pdb` file.
