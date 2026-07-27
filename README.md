# Marching Cubes Experiment

This Godot project serves as an experiment into implementing the marching cubes algorithm. The lookup tables were taken from Paul Bourke's article https://paulbourke.net/geometry/polygonise/.

## Summary

For a summary of the code, the implementation defines a uniform grid and a iso-value. It then goes through every cube within the uniform grid, taking samples at each of the 8 corners. 
The function being sampled can be anything, but in this case we've defined it to be a signed distance function (SDF) of a sphere with added noise. Using these samples, the implementation computes a cube 
index through bitmasking, which is then used to look up the intersected edges and triangle topology from the Marching Cubes lookup tables. To better approximate the isosurface, the algorithm linearly 
interpolates between the scalar values at the endpoints of each intersected edge, producing a more accurate estimate of the surface intersection point than simply using the edge midpoint.

Normals are calculated at a vertex by averaging the normals of the triangles which share that vertex.

<img width="541" height="515" alt="image" src="https://github.com/user-attachments/assets/64613777-784c-4c6d-86a7-019a5ec442c0" />
