# exp
Experimental useful codes.

URandom - is better random for Unity
  * excepting last value
  * using all values randomly
  > How to use
  
        private URandom _rand;
      private void Start()
      {
        _rand = new URandom(-1, 1);
      }

      private void Update()
      {
        if (Input.anyKeyDown)
          Debug.Log(_rand.Next());
      }
