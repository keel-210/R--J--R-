﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Mesurement
{
    static public Vector3 MesureNormal(Transform tra, Collision collision, LayerMask mask)
    {
        Vector3 normal = Vector3.zero;
        Vector3 centerPos = Vector3.zero, UpPos = Vector3.zero, BackPos = Vector3.zero;
        Vector3 contact = collision.contacts[collision.contacts.Length - 1].point;
        RaycastHit hit;
        if (Physics.Raycast(tra.position, contact - tra.position, out hit, 5f, mask))
        {
            centerPos = hit.point;
        }
        if (Physics.Raycast(tra.position - tra.right * 0.5f, contact - tra.position, out hit, 5f, mask))
        {
            BackPos = hit.point;
        }
        if (Physics.Raycast(tra.position + tra.up * 0.5f, contact - tra.position, out hit, 5f, mask))
        {
            UpPos = hit.point;
        }
        if (centerPos != Vector3.zero && BackPos != Vector3.zero && UpPos != Vector3.zero)
        {
            Vector3 dir1 = BackPos - centerPos;
            Vector3 dir2 = UpPos - centerPos;
            if (dir1 != Vector3.zero || dir2 != Vector3.zero)
            {
                normal = Vector3.Cross(dir1, dir2).normalized;
                if (normal == Vector3.zero)
                {
                    normal = Vector3.up;
                    return normal;
                }
                if (Physics.Raycast(tra.position, -normal, out hit, 1f, mask))
                {
                    return normal;
                }
                else
                {
                    normal = Vector3.Cross(dir2, dir1).normalized;
                    return normal;
                }
            }
        }
        Debug.DrawRay(tra.position, contact - tra.position, Color.blue);
        Debug.DrawRay(tra.position - tra.right * 0.5f, contact - tra.position, Color.blue);
        Debug.DrawRay(tra.position + tra.up * 0.5f, contact - tra.position, Color.blue);

        Debug.DrawRay(tra.position, normal * 5, Color.black);
        return normal;
    }
    static public Vector3 MesureDirection(Transform tra, Collision collision, LayerMask mask, Vector3 direction)
    {
        Vector3 dir = tra.right;
        Vector3 Pos1 = Vector3.zero, Pos2 = Vector3.zero, Pos3 = Vector3.zero, Pos4 = Vector3.zero;
        Vector3 contact = collision.contacts[collision.contacts.Length - 1].point;
        RaycastHit hit;
        Vector3 Offset1 = Vector3.Cross(tra.right, direction).normalized * 0.25f;
        Debug.DrawRay(tra.position, contact, Color.magenta);
        Debug.DrawRay(tra.position, Offset1, Color.magenta);
        Vector3 right = tra.right * 0.25f;
        Vector3 forward = tra.forward * 0.25f;
        if (Physics.Raycast(tra.position + Offset1 + right, direction, out hit, 5f, mask))
        {
            Pos1 = hit.point;
        }
        if (Physics.Raycast(tra.position + Offset1 - right, direction, out hit, 5f, mask))
        {
            Pos2 = hit.point;
        }
        if (Physics.Raycast(tra.position - Offset1 + right, direction, out hit, 5f, mask))
        {
            Pos3 = hit.point;
        }
        if (Physics.Raycast(tra.position - Offset1 - right, direction, out hit, 5f, mask))
        {
            Pos4 = hit.point;
        }
        if (Pos1 != Vector3.zero && Pos2 != Vector3.zero && Pos3 != Vector3.zero && Pos4 != Vector3.zero)
        {
            Vector3 dir1 = Pos1 - Pos2;
            Vector3 dir2 = Pos3 - Pos2;
            Vector3 dir3 = Pos1 - Pos4;
            Vector3 dir4 = Pos3 - Pos4;
            dir = (dir1 + dir2 + dir3 + dir4).normalized;
            dir = new Vector3(dir.x, 0, dir.z).normalized;
        }
        else
        {
            if (Pos1 == Vector3.zero && Pos2 == Vector3.zero && Pos3 == Vector3.zero && Pos4 == Vector3.zero)
            {
                if (Physics.Raycast(tra.position + Offset1 + right, -direction, out hit, 5f, mask))
                {
                    Pos1 = hit.point;
                }
                if (Physics.Raycast(tra.position + Offset1 - right, -direction, out hit, 5f, mask))
                {
                    Pos2 = hit.point;
                }
                if (Physics.Raycast(tra.position - Offset1 + right, -direction, out hit, 5f, mask))
                {
                    Pos3 = hit.point;
                }
                if (Physics.Raycast(tra.position - Offset1 - right, -direction, out hit, 5f, mask))
                {
                    Pos4 = hit.point;
                }
                if (Pos1 != Vector3.zero && Pos2 != Vector3.zero && Pos3 != Vector3.zero && Pos4 != Vector3.zero)
                {
                    Vector3 dir1 = Pos1 - Pos2;
                    Vector3 dir2 = Pos3 - Pos2;
                    Vector3 dir3 = Pos1 - Pos4;
                    Vector3 dir4 = Pos3 - Pos4;
                    dir = (dir1 + dir2 + dir3 + dir4).normalized;
                    dir = new Vector3(dir.x, 0, dir.z).normalized;
                }
            }
            else
            {
                Debug.Log("Direction Mesurement Error" + direction + collision.gameObject.name);
            }
        }

        Debug.DrawRay(tra.position + Offset1 + right, direction, Color.cyan);
        Debug.DrawRay(tra.position + Offset1 - right, direction, Color.cyan);
        Debug.DrawRay(tra.position - Offset1 - right, direction, Color.cyan);
        Debug.DrawRay(tra.position - Offset1 + right, direction, Color.cyan);

        Debug.DrawRay(tra.position, dir * 5, Color.red);
        return dir;
    }
    static public Vector3 RayContactPoint(Vector3 start, Vector3 dir, LayerMask mask)
    {
        Vector3 contactPoint = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit, 5f, mask))
        {
            contactPoint = hit.point;
        }
        return contactPoint;
    }
}
