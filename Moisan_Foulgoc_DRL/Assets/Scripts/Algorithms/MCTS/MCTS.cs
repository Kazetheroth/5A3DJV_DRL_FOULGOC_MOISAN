using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using TicTacTard;
using UnityEngine;

namespace Algorithms.MCTS
{
    public class Tree
    {
        private Node root;

        public Tree(Node root)
        {
            this.root = root;
        }

        public Node Root
        {
            get => root;
            set => root = value;
        }
    }
    public class Node
    {
        private IGameState state;
        private Node parent;
        private List<Node> childs;

        public Node(TicTacTardState state, Node parent, List<Node> childs)
        {
            this.state = state;
            this.parent = parent;
            this.childs = childs;
        }

        public IGameState State
        {
            get => state;
            set => state = value;
        }

        public Node Parent
        {
            get => parent;
            set => parent = value;
        }

        public List<Node> Childs
        {
            get => childs;
            set => childs = value;
        }
    }
    
    
    
    public class MCTS
    {
        private const int WIN_SCORE = 10;
        private const int MAX_PLAYS = 100;
        

        public List<List<ICell>> FindNextMove(List<List<ICell>> grid)
        {
            Tree tree = new Tree(new Node(new TicTacTardState(), null, new List<Node>()));
            Node rootNode = tree.Root;
            rootNode.State.SetCells(grid);
            
            int plays = 0;
            while (plays < MAX_PLAYS)
            {
                plays++;
            }
            
            throw new System.NotImplementedException();
        }
        
    }
}