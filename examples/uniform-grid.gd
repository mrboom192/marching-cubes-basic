@tool

extends Node3D

var density = 25;
var spacing = 1.0;

const shader = preload("res://shaders/scalar-field-visualization.gdshader")

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var noise = FastNoiseLite.new()
	noise.frequency = 0.05
	
	var sphere_mesh = SphereMesh.new()
	sphere_mesh.radial_segments = 2
	sphere_mesh.rings = 1
	
	# Create a uniform grid
	# Interpolate between bounding box corners
	for x in range(density):
		for y in range(density):
			for z in range(density):
				var v = Vector3(x, y, z) * spacing
				
				var point = MeshInstance3D.new()
				point.cast_shadow = false
				point.mesh = sphere_mesh
				point.position = v
				point.scale *= 0.25
				
				var val = noise.get_noise_3dv(v)
				
				var mat = ShaderMaterial.new()
				mat.shader = shader
				mat.set_shader_parameter("position", v)

				point.material_override = mat
				
				if(val < 0.0):
					add_child(point)
