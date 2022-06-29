using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node {
        // State
        Node parent;
        State state;
        List<Node> childArray;

        public Node()
        {
            this.state = new State();
            this.childArray = new List<Node>();
        }

        public Node(State state)
        {
            this.state = state;
            childArray = new List<Node>();
        }

        public Node(State state, Node parent, List<Node> childArray)
        {
            this.state = state;
            this.parent = parent;
            this.childArray = childArray;
        }

        public Node(Node node)
        {
            this.childArray = new List<Node>();
            this.state = new State(node.GetState());
        }

        public State GetState()
        {
            return state;
        }

        public void SetState(State state)
        {
            this.state = state;
        }

        public Node GetParent()
        {
            return parent;
        }

        public void SetParent(Node parent)
        {
            this.parent = parent;
        }

        public List<Node> GetChildArray()
        {
            return childArray;
        }

        public void SetChildArray(List<Node> childArray)
        {
            this.childArray = childArray;
        }

        public Node GetRandomChildNode()
        {
            int noOfPossibleMoves = this.childArray.Count;
            int selectRandom = (int)(Random.Range(0, noOfPossibleMoves));
            return this.childArray[selectRandom];
        }

        //todo Show dad
        public Node GetChildWithMaxScore()
        {
            var bestScore = this.childArray.OrderByDescending(child => child.GetState().getVisitCount()).FirstOrDefault();
            return bestScore;
        }
}
