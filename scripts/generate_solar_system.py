import csv
import math
from pathlib import Path


# ------------------------------------------------------------
# Configuration
# ------------------------------------------------------------

DURATION = 20.0       # total simulated seconds
SAMPLE_RATE = 10      # samples per second
DT = 1.0 / SAMPLE_RATE

SUN_SPEED = 0.25


# object_id, orbit radius, orbit period (seconds), initial phase (radians)
objects = [
    ("sun",     0.0, None, 0.0),
    ("mercury", 0.8, 6.0,  0.0),
    ("venus",   1.2, 9.0,  0.8),
    ("earth",   1.7, 12.0, 1.6),
    ("mars",    2.2, 16.0, 2.4),
]


# ------------------------------------------------------------
# Output path
# ------------------------------------------------------------

script_directory = Path(__file__).resolve().parent
project_root = script_directory.parent

output_directory = project_root / "data"
output_directory.mkdir(exist_ok=True)

output_file = output_directory / f"solar_system_{SAMPLE_RATE}Hz_{int(DURATION)}s.csv"


# ------------------------------------------------------------
# Generate CSV
# ------------------------------------------------------------

row_count = 0

with output_file.open("w", newline="", encoding="utf-8") as file:
    writer = csv.writer(file)

    writer.writerow([
        "timestamp",
        "object_id",
        "x",
        "y",
        "z"
    ])

    sample_count = int(DURATION * SAMPLE_RATE) + 1

    for sample in range(sample_count):
        timestamp = sample * DT

        # The Sun moves in a straight line along the X axis.
        sun_x = SUN_SPEED * timestamp

        for object_id, radius, period, phase in objects:

            if object_id == "sun":
                x = sun_x
                y = 0.0
                z = 0.0

            else:
                angular_speed = 2.0 * math.pi / period
                angle = angular_speed * timestamp + phase

                # Position relative to the current position of the Sun.
                x = sun_x
                y = radius * math.cos(angle)
                z = radius * math.sin(angle)

            writer.writerow([
                f"{timestamp:.3f}",
                object_id,
                f"{x:.4f}",
                f"{y:.4f}",
                f"{z:.4f}"
            ])

            row_count += 1


print(f"Generated: {output_file}")
print(f"Data rows: {row_count}")