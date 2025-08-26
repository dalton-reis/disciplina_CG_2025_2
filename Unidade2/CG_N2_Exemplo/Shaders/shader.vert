#version 330 core

layout(location = 0) in vec3 aPosition;

// Add a uniform for the transformation matrix.
uniform mat4 transform;

void main(void)
{
    // Then all you have to do is multiply the vertices by the transformation matrix, and you'll see your transformation in the scene!
    gl_Position = vec4(aPosition, 1.0) * transform;
}