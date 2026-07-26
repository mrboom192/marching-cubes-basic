extends Node

var levels = 10

func marching_cubes():
	# Process 8 samples at a time in an imaginary cube
	for x in range(levels):
		for y in range(levels):
			for z in range(levels):
				# Represent corners of imaginary cube
				var cube_corners: Array = [
					Vector3(x + 0, y + 0, z + 0),
					Vector3(x + 1, y + 0, z + 0),
					Vector3(x + 0, y + 1, z + 0),
					Vector3(x + 1, y + 1, z + 0),
					Vector3(x + 0, y + 0, z + 1),
					Vector3(x + 1, y + 0, z + 1),
					Vector3(x + 0, y + 1, z + 1),
					Vector3(x + 1, y + 1, z + 1),
				]
				
				
				pass

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
