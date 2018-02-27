### Ray

A Ray object is rendered as a cylinder.  It's length will be be defined by the first valid (not ray-transparent) target along its length.  It will call RayHit on all objects along its length that implement IRayTarget.

### RaySource

A RaySource will create a ray object.

### Mirror : IRayTarget

Produces a new ray based on the normal at the point it hits.