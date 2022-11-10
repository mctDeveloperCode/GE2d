using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MatisynGame;

public class Game1 : Game
{
    private const int COUNT = 9;

    private Dictionary<string, Texture2D> graphics = new ();
    private List<Descriptor> descriptors = new ();
    private int active = 0;


    sealed class Descriptor
    {
        public Texture2D graphic;
        public Vector2 graphicPosition => new Vector2(position.X - graphic.Width / 2.0f, position.Y - graphic.Height / 2.0f);
        public float size => (graphic.Height + graphic.Width) / 2.0f;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle box => new ((int) position.X, (int) position.Y, graphic.Width, graphic.Height);

        public Descriptor(Texture2D graphic, Vector2 position, Vector2 velocity) =>
            (this.graphic, this.position, this.velocity) = (graphic, position, velocity);
    }


    private GraphicsDeviceManager _graphics;
    [AllowNull] private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();

        int height = 900;
        int width = 1350;

        _graphics.PreferredBackBufferHeight = height;
        _graphics.PreferredBackBufferWidth = width;
        _graphics.ApplyChanges();

        for (int n = 0; n < COUNT; n++)
            descriptors.Add(new Descriptor(graphics["House"], new Vector2(100 + 100 * n, 100), new Vector2(10000, 10000)));
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        graphics["A"] = Content.Load<Texture2D>("TheA");
        graphics["B"] = Content.Load<Texture2D>("TheB");
        // graphics["C"] = Content.Load<Texture2D>("TheC");
        graphics["D"] = Content.Load<Texture2D>("TheD");
        graphics["E"] = Content.Load<Texture2D>("TheE");
        // graphics["F"] = Content.Load<Texture2D>("TheF");
        // graphics["G"] = Content.Load<Texture2D>("TheG");
        graphics["H"] = Content.Load<Texture2D>("TheH");
        graphics["I"] = Content.Load<Texture2D>("TheI");
        // graphics["J"] = Content.Load<Texture2D>("TheJ");
        graphics["K"] = Content.Load<Texture2D>("TheK");
        graphics["L"] = Content.Load<Texture2D>("TheL");
        graphics["M"] = Content.Load<Texture2D>("TheM");
        graphics["N"] = Content.Load<Texture2D>("TheN");
        graphics["O"] = Content.Load<Texture2D>("TheO");
        graphics["P"] = Content.Load<Texture2D>("TheP");
        // graphics["Q"] = Content.Load<Texture2D>("TheQ");
        // graphics["R"] = Content.Load<Texture2D>("TheR");
        graphics["S"] = Content.Load<Texture2D>("TheS");
        graphics["T"] = Content.Load<Texture2D>("TheT");
        // graphics["U"] = Content.Load<Texture2D>("TheU");
        // graphics["V"] = Content.Load<Texture2D>("TheV");
        // graphics["W"] = Content.Load<Texture2D>("TheW");
        // graphics["X"] = Content.Load<Texture2D>("TheX");
        graphics["Y"] = Content.Load<Texture2D>("TheY");
        // graphics["Z"] = Content.Load<Texture2D>("TheZ");

        graphics["House"] = Content.Load<Texture2D>("House");
        graphics["HouseyHousey"] = Content.Load<Texture2D>("HouseyHousey");
        graphics["AmongUsRainbow"] = Content.Load<Texture2D>("AmongUsRainbow");
    }

    private bool IsShifted() =>
        Keyboard.GetState().IsKeyDown(Keys.LeftShift) ||
        Keyboard.GetState().IsKeyDown(Keys.RightShift);

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Pause while backspace key is held down
        if (Keyboard.GetState().IsKeyDown(Keys.Back))
            return;

        // Drop frames that are slower than 20 frames per second
        if (gameTime.ElapsedGameTime.TotalSeconds > 0.05)
            return;

        if (active < descriptors.Count)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                descriptors[active].velocity += new Vector2(10, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                descriptors[active].velocity += new Vector2(-10, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                descriptors[active].velocity += new Vector2(0, -10);

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                descriptors[active].velocity += new Vector2(0, 10);

            if (Keyboard.GetState().IsKeyDown(Keys.D6) && IsShifted())
                descriptors[active].graphic = graphics["House"];

            if (Keyboard.GetState().IsKeyDown(Keys.D7) && IsShifted())
                descriptors[active].graphic = graphics["HouseyHousey"];
        }

        if (Keyboard.GetState().IsKeyDown(Keys.D0) && !IsShifted())
            descriptors[active].velocity = new Vector2(0, 0);

        if (Keyboard.GetState().IsKeyDown(Keys.D1) && !IsShifted())
            active = 0;

        if (Keyboard.GetState().IsKeyDown(Keys.D2) && !IsShifted())
            active = 1;

        if (Keyboard.GetState().IsKeyDown(Keys.D3) && !IsShifted())
            active = 2;

        if (Keyboard.GetState().IsKeyDown(Keys.D4) && !IsShifted())
            active = 3;

        if (Keyboard.GetState().IsKeyDown(Keys.D5) && !IsShifted())
            active = 4;

        if (Keyboard.GetState().IsKeyDown(Keys.D6) && !IsShifted())
            active = 5;

        if (Keyboard.GetState().IsKeyDown(Keys.D7) && !IsShifted())
            active = 6;

        if (Keyboard.GetState().IsKeyDown(Keys.D8) && !IsShifted())
            active = 7;

        if (Keyboard.GetState().IsKeyDown(Keys.D9) && !IsShifted())
            active = 8;


        foreach (Keys key in Keyboard.GetState().GetPressedKeys())
        {
            if (graphics.TryGetValue(key.ToString(), out Texture2D? graphic))
            {
                descriptors[active].graphic = graphic;
                break;
            }
        }

        foreach (var descriptor in descriptors)
        {
            foreach (var other in descriptors)
            {
                if (object.ReferenceEquals(other, descriptor))
                    continue;

                Vector2 diff = other.position - descriptor.position;
                float overlap = (other.size + descriptor.size) / 2.0f - diff.Length();
                if (overlap > 0.0001f)
                {
                    Vector2 parallel = diff;
                    parallel.Normalize(); // points toward other

                    // Adjust position
                    float distance = overlap / 2.0f;
                    descriptor.position -= parallel * distance;
                    other.position += parallel * distance;

                    // Adjust velocity
                    Vector2 v1i = descriptor.velocity;
                    Vector2 v2i = other.velocity;

                    Vector2 vdiff12 = v1i - v2i;
                    Vector2 pdiff12 = descriptor.position - other.position;

                    Vector2 v1f = v1i - Vector2.Dot(vdiff12, pdiff12) / pdiff12.LengthSquared() * pdiff12;
                    Vector2 v2f = v2i - Vector2.Dot(-vdiff12, -pdiff12) / pdiff12.LengthSquared() * -pdiff12;

                    descriptor.velocity = v1f * 0.98f;
                    other.velocity = v2f * 0.98f;
                }
            }

            descriptor.position += descriptor.velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;

            // Right stop
            if (descriptor.position.X > _graphics.PreferredBackBufferWidth - descriptor.graphic.Width / 2.0f)
            {
                descriptor.position = new Vector2(_graphics.PreferredBackBufferWidth - descriptor.graphic.Width / 2.0f, descriptor.position.Y);
                descriptor.velocity = new Vector2(-descriptor.velocity.X, descriptor.velocity.Y);
            }

            // Bottom stop
            if (descriptor.position.Y > _graphics.PreferredBackBufferHeight - descriptor.graphic.Height / 2.0f)
            {
                descriptor.position = new Vector2(descriptor.position.X, _graphics.PreferredBackBufferHeight - descriptor.graphic.Height / 2.0f);
                descriptor.velocity = new Vector2(descriptor.velocity.X, -descriptor.velocity.Y);
            }

            // Left stop
            if (descriptor.position.X < descriptor.graphic.Width / 2.0f)
            {
                descriptor.position = new Vector2(descriptor.graphic.Width / 2.0f, descriptor.position.Y);
                descriptor.velocity = new Vector2(-descriptor.velocity.X, descriptor.velocity.Y);
            }

            // Top stop
            if (descriptor.position.Y < descriptor.graphic.Height / 2.0f)
            {
                descriptor.position = new Vector2(descriptor.position.X, descriptor.graphic.Height / 2.0f);
                descriptor.velocity = new Vector2(descriptor.velocity.X, -descriptor.velocity.Y);
            }

            ke += descriptor.velocity.LengthSquared();
        }

        if (count % 60 == 0)
        {
            ke /= 60 * 9;
            Debug.WriteLine($"ke: {ke:0.00e00}   dt: {gameTime.ElapsedGameTime.TotalSeconds}");
            ke = 0;
        }
        count++;

        base.Update(gameTime);
    }

    private float ke = 0.0f;
    int count = 0;

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGreen);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        // _spriteBatch.Draw(
        //     texture: TheK,
        //     position: TheGraphicPosition,
        //     color: Color.White);
        // _spriteBatch.Draw(
        //     texture: TheN,
        //     destinationRectangle: new Rectangle(100, 100, 96, 96),
        //     color: Color.White);

        foreach (var descriptor in descriptors)
        {
            _spriteBatch.Draw(
                texture: descriptor.graphic,
                position: descriptor.position - new Vector2(descriptor.graphic.Width / 2.0f, descriptor.graphic.Height / 2.0f),
                color: Color.White);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
